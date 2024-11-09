Feature: Registration

This feature will fail now. Because there is no proper "before" feature
and browser is not created.

@tag1
Scenario: [scenario name]
	Given [context]
	When [action]
	Then [outcome]


  Scenario Outline: Correct user is able to create an account without transformer
    Given following user <userId> from <fileName> in <path>
    And User <userId> is on register page
    And User fills all fields on registration page
    When User presses the register button
    Then Process ends with a sentence "Your registration completed"
   # And User clicks continue button
   # And User "<userId>" is able to login on login page with login and password
   # And Correct data are displayed in user page
  Examples:
    | fileName   | userId      | path     |
    | users.json | defaultUser | E:\data\ |