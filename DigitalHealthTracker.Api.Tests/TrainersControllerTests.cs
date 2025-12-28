using Microsoft.AspNetCore.Mvc.Testing;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Xunit;

namespace DigitalHealthTracker.Api.Tests;

public class TrainersControllerTests : IClassFixture<CustomWebApplicationFactory>
{
	private readonly HttpClient _client;
	private readonly CustomWebApplicationFactory _factory;

	public TrainersControllerTests(CustomWebApplicationFactory factory)
	{
		// Save factory so we can seed test data into the in-memory database
		_factory = factory;

		// Create an HttpClient that calls our API in the test host
		_client = factory.CreateClient(new WebApplicationFactoryClientOptions
		{
			AllowAutoRedirect = false
		});
	}

	[Fact]
	public async Task GetAll_Trainers_Returns_200_OK()
	{
		// This test checks: GET /api/Trainers should respond with HTTP 200
		var response = await _client.GetAsync("/api/Trainers");
		Assert.Equal(HttpStatusCode.OK, response.StatusCode);
	}

	[Fact]
	public async Task GetPending_Trainers_Returns_Empty_List()
	{
		// This test checks: when there is no pending trainer, GET /api/Trainers/pending returns 200 and []

		var response = await _client.GetAsync("/api/Trainers/pending");
		Assert.Equal(HttpStatusCode.OK, response.StatusCode);

		var list = await response.Content.ReadFromJsonAsync<List<object>>();
		Assert.NotNull(list);
		Assert.Empty(list!);
	}

	[Fact]
	public async Task GetAll_Trainers_Returns_NonEmpty_List_When_Seeded()
	{
		// This test checks: after we add a trainer into DB (seed), GET /api/Trainers should return a non-empty list

		// Arrange: add a trainer into the test database
		_factory.SeedTrainer();

		// Act: call the endpoint
		var resp = await _client.GetAsync("/api/Trainers");
		var body = await resp.Content.ReadAsStringAsync();

		// Assert: request is successful and list has at least 1 item
		Assert.True(resp.IsSuccessStatusCode, $"HTTP {(int)resp.StatusCode}. Body: {body}");

		var list = await resp.Content.ReadFromJsonAsync<List<object>>();
		Assert.NotNull(list);
		Assert.NotEmpty(list!);
	}


	[Fact]
	public async Task Approve_Removes_Trainer_From_Pending_List()
	{
		// This test checks: after approve, the trainer should NOT appear in /api/Trainers/pending

		// Arrange: seed a pending trainer (IsApproved = false)
		_factory.SeedTrainer(name: "Approve", surname: "Me", phone: "5551111111");

		// Get the trainer id from GET /api/Trainers (we take the last created one)
		var allResp = await _client.GetAsync("/api/Trainers");
		var allBody = await allResp.Content.ReadAsStringAsync();
		Assert.True(allResp.IsSuccessStatusCode, allBody);

		using var doc = JsonDocument.Parse(allBody);
		var arr = doc.RootElement;

		Assert.Equal(JsonValueKind.Array, arr.ValueKind);
		Assert.True(arr.GetArrayLength() > 0, $"Expected non-empty array. Body: {allBody}");

		var last = arr[arr.GetArrayLength() - 1];
		var id = last.GetProperty("id").GetInt32();

		// Act: approve the trainer
		var approveResp = await _client.PutAsync($"/api/Trainers/{id}/approve", null);
		var approveBody = await approveResp.Content.ReadAsStringAsync();
		Assert.True(approveResp.IsSuccessStatusCode, approveBody);

		// Assert: pending list should not contain this id anymore
		var pendingResp = await _client.GetAsync("/api/Trainers/pending");
		var pendingBody = await pendingResp.Content.ReadAsStringAsync();
		Assert.True(pendingResp.IsSuccessStatusCode, pendingBody);

		Assert.DoesNotContain($"\"id\":{id}", pendingBody);
	}

	[Fact]
	public async Task Update_Trainer_Changes_Fields()
	{
		// This test checks: PUT /api/Trainers/{id} updates the trainer fields (name, surname, phone, email)

		// Arrange: create a trainer to update
		_factory.SeedTrainer(name: "Old", surname: "Name", phone: "5552222222");

		// Get trainer id
		var allResp = await _client.GetAsync("/api/Trainers");
		var allBody = await allResp.Content.ReadAsStringAsync();
		Assert.True(allResp.IsSuccessStatusCode, allBody);

		using var doc = JsonDocument.Parse(allBody);
		var arr = doc.RootElement;
		var last = arr[arr.GetArrayLength() - 1];
		var id = last.GetProperty("id").GetInt32();

		// New values we want to set
		var payload = new
		{
			name = "NewName",
			surname = "NewSurname",
			phone = "5553333333",
			email = "new@mail.com"
		};

		// Act: update trainer
		var putResp = await _client.PutAsJsonAsync($"/api/Trainers/{id}", payload);
		var putBody = await putResp.Content.ReadAsStringAsync();
		Assert.True(putResp.IsSuccessStatusCode, putBody);

		// Assert: GET by id should return the updated values
		var getResp = await _client.GetAsync($"/api/Trainers/{id}");
		var getBody = await getResp.Content.ReadAsStringAsync();
		Assert.True(getResp.IsSuccessStatusCode, getBody);

		using var doc2 = JsonDocument.Parse(getBody);
		var obj = doc2.RootElement;

		Assert.Equal("NewName", obj.GetProperty("name").GetString());
		Assert.Equal("NewSurname", obj.GetProperty("surname").GetString());
		Assert.Equal("5553333333", obj.GetProperty("phone").GetString());
		Assert.Equal("new@mail.com", obj.GetProperty("email").GetString());
	}

	[Fact]
	public async Task Delete_Trainer_Without_Logs_Returns_Ok_And_Removes_It()
	{
		// This test checks: if a trainer has no logs, DELETE /api/Trainers/{id} should succeed
		// and then GET /api/Trainers/{id} should return 404

		// Arrange: create a trainer without logs
		_factory.SeedTrainer(name: "Del", surname: "NoLogs", phone: "5554444444");

		// Get trainer id
		var allResp = await _client.GetAsync("/api/Trainers");
		var allBody = await allResp.Content.ReadAsStringAsync();
		Assert.True(allResp.IsSuccessStatusCode, allBody);

		using var doc = JsonDocument.Parse(allBody);
		var arr = doc.RootElement;
		var id = arr[arr.GetArrayLength() - 1].GetProperty("id").GetInt32();

		// Act: delete
		var delResp = await _client.DeleteAsync($"/api/Trainers/{id}");
		var delBody = await delResp.Content.ReadAsStringAsync();
		Assert.True(delResp.IsSuccessStatusCode, delBody);

		// Assert: trainer should not exist anymore
		var getResp = await _client.GetAsync($"/api/Trainers/{id}");
		Assert.Equal(HttpStatusCode.NotFound, getResp.StatusCode);
	}

	[Fact]
	public async Task Delete_Trainer_With_WorkoutLogs_Also_Deletes_Logs_And_Succeeds()
	{
		// This test checks: if a trainer has WorkoutLogs, DELETE should still succeed
		// because API should remove related logs first (cascade-like behavior)

		// Arrange: create a trainer
		_factory.SeedTrainer(name: "Del", surname: "WithLogs", phone: "5555555555");

		// Get trainer id
		var allResp = await _client.GetAsync("/api/Trainers");
		var allBody = await allResp.Content.ReadAsStringAsync();
		Assert.True(allResp.IsSuccessStatusCode, allBody);

		using var doc = JsonDocument.Parse(allBody);
		var arr = doc.RootElement;
		var id = arr[arr.GetArrayLength() - 1].GetProperty("id").GetInt32();

		// Add a WorkoutLog that points to this trainer (creates FK relationship)
		_factory.SeedWorkoutLogForTrainer(id);

		// Act: delete trainer
		var delResp = await _client.DeleteAsync($"/api/Trainers/{id}");
		var delBody = await delResp.Content.ReadAsStringAsync();
		Assert.True(delResp.IsSuccessStatusCode, delBody);

		// Assert: trainer should not exist anymore
		var getResp = await _client.GetAsync($"/api/Trainers/{id}");
		Assert.Equal(HttpStatusCode.NotFound, getResp.StatusCode);
	}

	[Fact]
	public async Task Delete_NonExisting_Trainer_Returns_404()
	{
		// This test checks: deleting a trainer that does not exist should return 404

		var nonExistingId = 999999;

		var resp = await _client.DeleteAsync($"/api/Trainers/{nonExistingId}");

		Assert.Equal(HttpStatusCode.NotFound, resp.StatusCode);
	}

	[Fact]
	public async Task GetById_NonExisting_Trainer_Returns_404()
	{
		// This test checks: GET /api/Trainers/{id} for a missing trainer should return 404

		var nonExistingId = 999999;

		var resp = await _client.GetAsync($"/api/Trainers/{nonExistingId}");

		Assert.Equal(HttpStatusCode.NotFound, resp.StatusCode);
	}

	[Fact]
	public async Task Unapprove_Moves_Trainer_Back_To_Pending_List()
	{
		// This test checks: approve + unapprove should move trainer back into pending list

		// Arrange: seed a trainer
		_factory.SeedTrainer(name: "Pending", surname: "Again", phone: "5559999999");

		// Get trainer id
		var allResp = await _client.GetAsync("/api/Trainers");
		var allBody = await allResp.Content.ReadAsStringAsync();
		Assert.True(allResp.IsSuccessStatusCode, allBody);

		using var doc = JsonDocument.Parse(allBody);
		var arr = doc.RootElement;
		var id = arr[arr.GetArrayLength() - 1].GetProperty("id").GetInt32();

		// Act 1: approve
		var approveResp = await _client.PutAsync($"/api/Trainers/{id}/approve", null);
		Assert.True(approveResp.IsSuccessStatusCode);

		// Act 2: unapprove
		var unapproveResp = await _client.PutAsync($"/api/Trainers/{id}/unapprove", null);
		Assert.True(unapproveResp.IsSuccessStatusCode);

		// Assert: trainer should appear in pending list again
		var pendingResp = await _client.GetAsync("/api/Trainers/pending");
		var pendingBody = await pendingResp.Content.ReadAsStringAsync();
		Assert.True(pendingResp.IsSuccessStatusCode, pendingBody);

		Assert.Contains($"\"id\":{id}", pendingBody);
	}

}
