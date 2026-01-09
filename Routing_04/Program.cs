using System.ComponentModel;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//app.MapGet("/", () => "Hello World!");

app.Map("map1",async (context) =>
{
       await context.Response.WriteAsync("Map 1");
});
app.Map("map2",async (context) =>
{
       await context.Response.WriteAsync("Map 2");
}); 

//now keep in mind the map middleware is added at the end of the pipeline so not to use the map.use after the map 

//or 👉 Map / MapGet / MapFallback endpoints pipeline ke end me execute hote hain
//Isliye unke baad app.Use(...) likhna useless hota hai.


//similarly if we can also use mappost, map get to invoke that endpoint middleware only for the get or post request like app.mapget("map3", ...), app.mappost("map4", ...)

//whichever part of the url can vary that are called the route parameteres also it can be represented with the curly braces like {id}, {name} etc, the fixed thing in the url is called the literal part of the url

//route parameter values are also called as route values 

app.Map("files/{filename}.{extension}", async (context) =>
{
    string? fileName = Convert.ToString( context.Request.RouteValues["filename"]);//important here that the route parameter returns the data in the object form so we have to convert it into the string format 
    //route parameter can never have a space 

    string? extension = Convert.ToString(context.Request.RouteValues["extension"]);
    await context.Response.WriteAsync(fileName+"."+extension);
});
app.MapFallback(async (context) =>
{
    await context.Response.WriteAsync("Fallback");
});


app.Run();
