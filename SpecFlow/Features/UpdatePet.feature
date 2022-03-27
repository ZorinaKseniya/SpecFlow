Feature: Update Pet
	As a person who want to sell a pet
	I want to be able to update information on it
	So that information is actual

@positive
Scenario Outline: update existing pet
    Given user has added his pet to store with one set of parameters
	When user tries to update all its parameters 
	Then all new parametres have been saved


