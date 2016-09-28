# KNOWLEAD WEBAPI REPO

## How to run

1. Exec in root "git submodule init" on root dir
2. Exec in root "git submodule update --recursive --remote"
3. Exec in root "dotnet restore"
4. Go to "cd src/Knowlead.WebApi/"
5. And then run it with "dotnet run"


### Result Type

* OkResult 200
* OkObjectResult(object) 200
    *   
* BadRequestResult 400
* BadRequestObjectResult(object) 400
* BadRequestObjectResult(modelState) 400
    *  
* NotFoundResult 404
* NotFoundObjectResult(object) 404
    *  
* EmptyResult 200
* UnauthorizedResult 401
* InternalServerErrorResult 500
