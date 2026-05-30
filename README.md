# DigitalHealthFALL2025-2026

# SE-410 Project Requirements

*Project Topic:* Digital Health and Fitness Tracker with Trainer Access

---

## 1. Functional Requirements

* *FR1:* The system shall allow users to register by their name, surname, telephone number, and email address.
* *FR2:* To set up the fitness profile of users, the system shall request personal health data (age, weight, height, medical record, etc.) from users.
* *FR3:* The system calculates the Body Mass Index (BMI) and shows how much weight the user needs to gain/lose to reach a healthy weight. The trainer shall have access to this information to track the progress of the user.
* *FR4:* The trainers shall be registered to the system after being approved by the admin to prevent faulty trainer registrations.
* *FR5:* The trainers shall assign workout schedules and programs to the users that they are responsible for.
* *FR6:* The user shall approve the schedules and programs assigned to them. After approval, the user shall access the activated program and set it as "completed" after the exercise is finished.
* *FR7:* Users and trainers should monitor users’ progress. Trainers will be able to send reminders when a user misses workouts.
* *FR8:* The users shall access their previously completed workouts. The trainers shall access the workouts they have previously assigned.
* *FR9:* The users shall manually enter the exercise they completed except their program predefined by their trainer.
* *FR10:* The admin shall access the database to add, update, and delete both users and trainers.

---

## 2. Non-Functional Requirements

* *NFR1:* The system shall connect to SQL Server for database management operations.
* *NFR2:* The system shall ensure all primary functions are accessible within at most five clicks.
* *NFR3:* The system shall have a responsive interface allowing access from different device models.
* *NFR4:* The system should support the English language.
* *NFR5:* To protect and store user data, a personal data protection agreement (KVKK) shall be approved by the user during registration.

---

## 3. Scenario

The user registers with the system by their name and contact information. After completing the registration, the user fills in the required health data form, consisting of information including but not limited to height, weight, and medical records. Users’ sensitive data is protected by the system and trainers.

When a trainer is hired, his/her registration is done by the admin to mitigate the risk of erroneous or unverified trainer entries. As well, the system calculates the BMI of the user and notifies them how much weight the user needs to gain/lose to reach a healthy weight.

When the fitness profile of a user is complete, the system automatically assigns the user to an available trainer. Trainers see all users' medical information who are assigned to them. The trainer creates an exercise routine according to the fitness profile and the aim of the user.

The fitness schedule and program will be available on the user profile after the trainer assigns them and the user approves. The user is able to mark the completed exercise as “checked”. Moreover, the user is allowed to enter the programs they completed in addition to their assigned program. Both users and trainers are allowed to access their previous exercises. When users miss their exercises, trainers notify them on the system. The user and the assigned trainers are allowed to track the progress of the user.
