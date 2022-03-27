Feature: Delete a User
	As a manager of pet store 
	I want to delete users
	So that only good users are stored

@positive
Scenario: deleting existing user 
    Given user has been created
	And petstore administrator is a logged in user
	When petstore administrator tries to delete a user
	Then user is deleted



@negative
Scenario: get 400 code when user with given id does not exist
    Given petstore administrator has not logged in
	When user try to find pet by id 55
	Then user get NotFound


