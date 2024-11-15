﻿Feature: ApiRegistration

Background:
    Given Following user "userForChecks"
    And "userForChecks" is created
    And 201 response code is received
    And Json in response body matches createdUser.json
    And Response object is properly validated as an user object of an user "userForChecks"
    When "userForChecks" starts to login with credentials
    Then 200 response code is received
    And Correct user object is received
    And Cookie can be obtained from response header


  Scenario Outline: Registration of an user without all data is possible (bug? of this software)
    Given Following user "<username>"
    When "<username>" is created with not all data provided in request based on "<file>"
    Then <status code> response code is received
    And Response object is validated with a file "<schema>"
    And Get request for users list is sent
    And Response contains "<username>"
    Examples:
      | username | file                                          | status code | username1 | schema                                                |
      | username | jsons/RegistrationInvalid/EmptyFirstName.json | 201         | username1 | jsons/RegistrationInvalid/schemas/EmptyFirstName.json |
      | username | jsons/RegistrationInvalid/EmptyLastname.json  | 201         | username1 | jsons/RegistrationInvalid/schemas/EmptyLastName.json  |

  Scenario Outline: Registration of an user without username is possible (bug? of this software)
    Given Following user "<username>"
    When "<username>" is created with not all data provided in request based on "<file>"
    Then <status code> response code is received
    And Response object is validated with a file "<schema>"
    And Get request for users list is sent
    And Response contains no username "<username>" for data from "<file>"
    Examples:
      | username | file                                          | status code | username1 | schema                                                |
      | username | jsons/RegistrationInvalid/EmptyUsername.json  | 201         | username1 | jsons/RegistrationInvalid/schemas/EmptyUserName.json  |

  Scenario: Registration of an user without password is not possible
    Given Following user "username"
    When "username" is created with not all data provided in request based on "jsons\\RegistrationInvalid\\EmptyPassword.json"
    Then 500 response code is received
    And Get request for users list is sent
    And Response does not contain object for "<username>"