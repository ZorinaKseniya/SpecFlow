@ignore
Feature: registration
  In order to plays Casino
  As a user
  I want to be able to register

Background:
	Given user is at the Home Page
	And navigates to Registration Page

@positive
Scenario:  SignUp only after confirming e-mail
    Given user has requested account confirmation per Email
	When user follows Confirmation link in the email
	Then user should be signed up

@positive
Scenario Outline:  SignUp with valid fields
	When user enters <e-mail> in email field
	And user enters <nickname> in nickname field
	And user enters <password> in password field
	And user enters <dateOfBirth> in dateOfBirth field
	And user confirms he is not a robot
	And user agrees with GTC
	And user clicks on Regisrter button
	Then account confirming window should display
	But user is still not signed up 

Examples:
  | E-mail                                         | nickName     | password                         | dateOfBirth         |
  | very.long.long.LONG.long.long.long@example.com | min          | 8symbols                         |<currentDate-18years>|
  | sh@o.rt                                        | 13symbolsMax | specialcharacters@#!             | 01-01-1900          |
  | 1$2*@numbersandsymbols.ru                      | BIGLETTERS   | verylongpasswordverylongpassword@| 31-12-1997          |

@negative
Scenario Outline: Failed SignUp for alreadt existing user
	Given user with <field> <data> already exists
	And user filles in all fields correctly
	But user enters <data> in field <field>
	And user clicks on Register button
	Then validation error with text <errorText> is shown
	And account confirming window should not display
    And user is still not signed up 

Examples:
  | field    | data                |errorText                                          |
  | nickName | AlreadyTakenName    | This nickname is already taken.                   |
  | e-Mail   | Already@Taken.Email | The e-mail address you entered is already in use. |

@negative 
Scenario: Failed SignUp for minors
	And user filles in all fields correctly
	But user is less then 18 years old
	And user clicks on Register button
	Then dateOfBirth validation error with text "The minimum legal age required for using our offerings is 18 years." is shown
	And account confirming window should not display 
	But user is still not signed up 

@negative 
Scenario Outline: Failed SignUp when obligatory field not filled
	And user filles in all fields correctly
    But user does not fill field <field>
	Then validation error with text <textOfError> should display
	And account confirming window should not display
    And user is still not signed up 

Examples:
  | field          |textOfError                                                   |
  | email          |E-mail address required                                       |
  | nickName       |Nickname required                                             |
  | password       |Password required                                             |
  | captcha        |The security check is a required field. Please enter the code.|
  | agree to terms |You must agree to our General Terms & Conditions to continue. |


@negative 
Scenario Outline: Failed SignUp when invalid format of field
	And user filles in all fields correctly
    But user does fills field <field> with invalid data <data>
	Then validation error with text <textOfError> should display
	And account confirming window should not display
	And user is still not signed up 

Examples:
  | field       | data              | textOfError                                                                        |
  | email       | nodomainname@     | Please enter a valid e-mail address                                                |
  | email       | @nousername.com   | Please enter a valid e-mail address                                                |
  | email       | username@nodot    | Please enter a valid e-mail address                                                |
  | email       | noAtSign.com      | Please enter a valid e-mail address                                                |
  | nickName    | 22                | Your nickname must be between 3 and 13 characters long.                            |
  | nickName    | 14141414141414    | Your nickname must be between 3 and 13 characters long.                            |
  | nickName    | specialsymbo!     | Your nickname may not contain Cyrillic letters or special characters.              |
  | nickName    | cyrillicФ         | Your nickname may not contain Cyrillic letters or special characters.              |
  | password    | 7symbol           | Your password must be at least 8 characters long.                                  |
  | password    | onlyLetters       | Your password must contain at least one letter, one number or a special character. |
  | dateOfBirth | 29-February-2002  | Invalid data.                                                                      |
  | captcha     | wrongImageSelected| Please try again                                                                   |
  
@positive
Scenario: user with not existing e-mail can not be registered
	Given no e-mail "thisEmailDoes@not.exists" exists
	When user filles in all fields correctly
	But user enters "thisEmailDoes@not.exists" in e-mail field
	And user clicks on Register button
	Then account confirming window should display
	And user is still not signed up 

