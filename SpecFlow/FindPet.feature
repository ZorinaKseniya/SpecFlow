Feature: Find Pet
	As a person who wants to buy a pet 
	I want to get information about them
	So I can decide if i like this pet

@positive
Scenario Outline: find existing pet by id
    Given i have added my pet to store with parameters <id>, <categoryId>, <categoryName>, <name>, <photoUrls>, <tagsId>, <tagsName>, <status>
	When i try to find them with id <idToSearch>
	Then i get my pet with correct parametres <id>, <categoryId>, <categoryName>, <name>, <photoUrls>, <tagsId>, <tagsName>, <status>

		  Examples:
		| id | categoryId | categoryName | name    | photoUrls | tagsId | tagsName | status    | idToSearch |
		| 33 | 1          | dog          | Bobik   | abc       | 7      | tag1     | available | 33         |
		| 77 | 2          | cat          | Kittie  | abc       | 6      | tag2     | pending   | 77         |
		| 99 | 2          | cat          | Basilio | abc       | 6      | tag3     | sold      | 99         |
		| 88 | 1          | <null>       | Bobik   | abc       | 7      | tag1     | available | 33         |
		| 11 |  <null>    | cat          | Basilio | abc       | 5      | null     | sold      | 11         |


Scenario: get 400 code when pet with given id doe
@negatives not exists
    Given i have not added pet to store with id 55
	When i try to find pet by id 55
	Then i get NotFound


