var builder = WebApplication.CreateBuilder(args);

// -----------------------------
// Reverse Proxy (YARP)
// -----------------------------
builder.Services.AddReverseProxy()
       .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

// -----------------------------
// Swagger / OpenAPI (?????????)
// -----------------------------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// -----------------------------
// HTTP request pipeline
// -----------------------------
if (app.Environment.IsDevelopment())
{
    // Swagger ?? ?-Dev
    app.UseSwagger();
    app.UseSwaggerUI();

    // ???? ?????? HTTPS redirect ?-Dev ?? ??? ?????
    // app.UseHttpsRedirection();
}

// -----------------------------
// Reverse Proxy Middleware
// -----------------------------
app.MapReverseProxy();

// -----------------------------
// Run the app
// -----------------------------
app.Run();
