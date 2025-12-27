using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DigitalHealthTracker.Web.Filters;

public class RequireRoleAttribute : ActionFilterAttribute
{
	private readonly string _role;

	public RequireRoleAttribute(string role)
	{
		_role = role;
	}

	public override void OnActionExecuting(ActionExecutingContext context)
	{
		var role = context.HttpContext.Session.GetString("Role");

		if (string.IsNullOrEmpty(role) || role != _role)
		{
			context.Result = new RedirectToActionResult("Login", "Account", null);
			return;
		}

		base.OnActionExecuting(context);
	}
}
