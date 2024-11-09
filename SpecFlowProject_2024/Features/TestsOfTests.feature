Feature: TestsOfTests

  Scenario Outline: Only for tests
    Given Following user "<username>"
    And "<username>" is created
    And 201 response code is received
    And "<username>" is created
    And 201 response code is received
    And "<username>" is created
    And 201 response code is received
    And Json in response body matches createdUser.json
    And Response object is properly validated as an user object of an user "<username>"
    When "<username>" starts to login with credentials
    Then 200 response code is received
    And Correct user object is received
    And Cookie can be obtained from response header
    And Seed of the database is performed
    Examples:
      | username        |
      | username1       |
      #| username        |

  Scenario Outline: Only for tests 2
    Given Seed of the database is performed
    Examples:
      | username        |
      | username1       |
      #| username        |