﻿Feature: Update Pet
	As a person who want to sell a pet
	I want to be able to update information on it
	So that information is actual

	@positive
Scenario Outline: update existing pet
    Given i have added my pet to store with one set of parameters
	When i try to update all its parameters 
	Then all new parametres have been saved


