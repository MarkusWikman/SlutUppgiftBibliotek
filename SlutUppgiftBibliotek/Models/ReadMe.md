Migration / Database Update / Build

The purpose of a migration is to create the database schema (tables/columns etc.) for our database.
Which database to update is controlled by the connection string in our Context class. If we don't want to use migrations,
we will need to manually create and/or change the database schema.

Entity framework creates the database schema/definition from the information given in Context.cs.
A migration-file contains the information needed about changes to make to the database, 
this can be new columns/tables/restrictions etc.

Creating the migration-file does not change the database, this is done when we run the `dotnet ef database update`

1. For our migrations work properly we need to make sure of these things:
- Our code changes are saved or solution is built
- Our context contains the DbSets needed if we have created new classes that we want to save in the DB
- Our code builds without errors
2. We need to make a new migration when we have made changes to the structure of our database, 
   either by adding new properties to existing classes or adding new classes altogether.
3. If our `dotnet ef migrations add initial` command fails, go to step 1.
4. To push the changes in the migration to the database, run `dotnet ef database update`
