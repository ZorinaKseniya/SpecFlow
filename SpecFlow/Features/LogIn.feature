@ignore
Feature: log in

  In order to access my account to play
  As a user of the website
  I want to log into the website

Background:
	Given user is at the Home Page
	And navigates to Login Page

@positive
Scenario Outline: Successful Manual Login with Valid Credentials
	Given user with <userNameDb> and Password "PasswordWithSymbols1&" is registered
	When user enters userName <userNameInput>
	And user enters Password "PasswordWithSymbols1&"
	And Clicks on the LogIn button
	Then Successful LogIn message should display

Examples:
  | userNameDb    | userNameInput |
  | 13symbolsMax1 | 13symbolsMax1 |
  | min           | min           |
  | CaSeChEcK     | casecheck     |
  | CaSeChEcK     | CASECHECK     |

@positive
Scenario Outline: Successful Automated Login with Valid Credentials
    Given registered user has decided to switch <on/off> LogIn Automatically feature
	And  browser is set to remember cookies
	When user closes site
	And user  visits the site again
	Then log in state is expected to be <logInState>

Examples:
  | on/of    |logInState      |
  | on       |logged in       |
  | off      |not logged in   |

@positive
Scenario: Showing Password
	Given user enters Password "PasswordWithSymbols1&"
	When user clicks on ShowPassword icon
	Then passoword "PasswordWithSymbols1&" is shown

@positive
Scenario: reseting password
	When user clicks on ForgottenYouPassword button
	Then passwordRecovery window should display

@positive
Scenario: registering from login window
	When user clicks on RegisterNow button
	Then registration window should display

@positive
Scenario: closing login window
	When userclicks on close button
	Then Home Page appears again

@negative
Scenario Outline: Non successful validation (wrong credentials)
	Given user with nickname "Agent07" and password "JamesBond" exists
	When user enters <nickname>  and <password>
	And clicks on the LogIn button
	Then <errorMessage> should display

Examples:
  | nickName | password   | errorMessage                              |
  | Agent08  | JamesBond  | Incorrect nickname/password combination.  |
  | Agent07  | JamesBond1 | Incorrect nickname/password combination.  |
  | Agent07  | JAMESBOND  | Incorrect nickname/password combination.  |
  | Agent07  |            | PasswordRequired                          |
  |          | JamesBond  | NicknameRequired                          |

