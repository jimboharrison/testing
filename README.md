# AcmeStudioRefactorTest_jimboharrison

## Plan

1. Make sure it is working with Swagger and DB runs
2. Extract and split out the InterfactWithDatabase class
3. Abstract any config, DB using IoC
4. Update endpoint naming
5. Check each endpoint is working as expcetd
6. 1 or 2 unit tests as an example

## Comments
1. Have added 2 example unit test classes. Having said that, there is little logic inside this API to be unit tested, other than mapping. It lends itself more to an integration test, using a proper database and allowing model binded validation attributes to be checked ( required fields for example

2. The tests are using the proper auto mapper profiles as it is easy to test and useful in unit tests. They are also using Moq for Db Context. We could abstract this further into repositories for example. Unit testing where EF Contexts are involved, is always a talking point. Should it be mocked , should it be an in memory DB, should it be a real DB?

3. The ServiceResult response type is useful but it not ideal where different handlers can return different response types. For instance, one handler may have 2 possible return types of Success and BadRequest, but a second handler may return 3 types of Success / NotFound / ValidationError. For this reason, given more time I'd like to create individual Response types for each of the handlers and remove the generic Service result class. It will create more code but ultimately mean stricter rules are enforced on the thin layer API controllers.

4. Again give more time, would like to create a seperate Database/Data project to house the entitiy framework configurations/models and migrations. For the sake of the test, it is fine inside a single project.

5. The handlers folder is probably better suited to live in an extracted 'PeoplesPartnership.ApiRefactor.ApplicationLayer' project, since it will have the business logic. The API project should act as a thin layer entry point into our service.


## Next steps with more time

1. Further validation of post and update models. Hard to do without business logic instructions, but should sold price always be below price for example?
2. Intergration tests to test inline model validation and database integration
3. Further abstraction of concerns as mentioned above.
4. Could think about logging / exceptions.
