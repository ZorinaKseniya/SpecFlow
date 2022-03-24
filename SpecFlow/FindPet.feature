Feature: Find Pet
	As a person who wants to buy a pet 
	I want to get information about them
	So I can decide if i like this pet

@positive
Scenario: find existing pet by id
    Given i have added my pet to store with id 33
	When i try to find him by id 33
	Then i get my pet with id 33

@negative
Scenario: get 400 code when pet with given id does not exists
    Given i have not added pet to store with id 55
	When i try to find pet by id 55
	Then i get NotFound


