using newIdManager.Extensions;
using newIdManager.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureAppIdentity(builder.Configuration);
builder.Services.ConfigureDatabase(builder.Configuration);
builder.Services.AddAppServices();

var app = builder.Build();

app.ConfigureExceptionHandler();
app.ConfigureMiddleware();

using (var scope = app.Services.CreateScope()) // 새로운 서비스 스코프(범위)를 생성 (일시적으로 서비스 사용 가능)
{
    var services = scope.ServiceProvider; // 스코프 내에서 서비스 제공자를 가져옴
    var seeder = services.GetRequiredService<Seeding>(); // 의존성 주입(DI)으로 Seeding 클래스 인스턴스 가져오기
    await seeder.SeedAsync(); // 비동기(Async) 방식으로 데이터베이스 초기화 실행
}

app.Run();
