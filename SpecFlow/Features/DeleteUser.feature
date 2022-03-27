Feature: Delete a User
	As a manager of pet store 
	I want to delete users
	So that only good users are stored

@positive
Scenario: deleting existing user 
    Given user with name "ExistingUser" has been created
	When petstore administrator tries to delete a user with name "ExistingUser"
	Then user is deleted



@negative
Scenario: get 400 code when user does not exist
    Given user with name "NotExistingUser" does not exist
	When petstore administrator tries to delete a user with name "NotExistingUser"
	Then user gets NotFound as User does not exists


