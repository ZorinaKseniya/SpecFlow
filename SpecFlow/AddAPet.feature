Feature: Add a pet
	As a seller of a pet
	I want to add him to the store
	So that somebody can buy it
	
@positive
Scenario: posting a pet 
	When i try to add my pet to store with id 33
	Then operation goes successfully
	And  my pet has been stored