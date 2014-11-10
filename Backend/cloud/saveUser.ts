// Force this TS file to become a module
export var x = 2;

Parse.Cloud.beforeSave("_User", (request, response) => {

    // Must have a player name
    if (request.object.get("playerName") == null)
        return response.error("Must supply a player name when creating a new user");

    // Email and username must equal
    if (request.object.get("email") != request.object.get("username"))
        return response.error("Username and email address must be equal");

    // All done
    response.success();
});