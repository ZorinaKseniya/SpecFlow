Feature: Find Pet
	As a person who wants to buy a pet 
	I want to get information about them
	So I can decide if i like this pet

@positive
Scenario Outline: find existing pet by id
    Given user has added his pet to store with parameters <id>, <categoryId>, <categoryName>, <name>, <photoUrls>, <tagsId>, <tagsName>, <status>
	When user tries to find him with id <idToSearch>
	Then user gets his pet with correct parametres <id>, <categoryId>, <categoryName>, <name>, <photoUrls>, <tagsId>, <tagsName>, <status>

		  Examples:
| note | id | categoryId | categoryName | name    | photoUrls | tagsId | tagsName | status    | idToSearch |
|      | 33 | 1          | dog          | Bobik   | abc       | 7      | tag1     | available | 33         |
|      | 77 | 2          | cat          | Kittie  | abc,def   | 6      | tag2     | pending   | 77         |
|      | 99 | 2          | cat          | Basilio | abc       | 6      | tag3     | sold      | 99         |
|      | 88 | 1          | <null>       | Bobik   | abc       | 7      | tag1     | available | 88         |
|	   | 11 | 0          | cat          | Basilio | abc       | 5      | <null>   | sold      | 11         |

@negative
Scenario: get 400 code when pet with given id does not exists
    Given user has not added pet to store with id 55
	When user tries to find pet by id 55
	Then user gets NotFound as pet does not exists


