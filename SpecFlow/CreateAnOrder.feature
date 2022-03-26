Feature: Create an order
	As a person who wants to buy a pet 
	I want to place my order
	So that i can buy a pet

@positive
Scenario Outline: creating an order
    Given a pet with <id> has been added to store
	When i make an order for that pet with parameters <id>, <petId>, <quantity>, <shipDate>, <status>, <complete>
	Then my order has been placed with parameters <id>, <petId>, <quantity>, <shipDate>, <status>, <complete>

		  Examples:
		| id  | petId | quantity | shipDate                 | status    | complete |
		| 100 | 1     | 2        | 2022-03-26T14:16:46.712Z | placed    | true     | 
		| 200 | 2     | 5        | 2022-03-26T14:16:46.712Z | approved  | false    | 
		| 300 | 3     | 0        | 2022-03-26T14:16:46.712Z | delivered | false    |
