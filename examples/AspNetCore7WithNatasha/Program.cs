//===================================================
//  License: Apache-2.0
//  Contributors: yiyungent@gmail.com
//  Project: https://moeci.com/PluginCore
//  GitHub: https://github.com/yiyungent/PluginCore
//===================================================



// ���� PluginCore ��������ռ�
using PluginCore.AspNetCore.Extensions;
using PluginCore.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 1. ��� PluginCore
builder.Services.AddTransient<IPluginContextPack, AspNetCore7WithNatasha.Natasha.NatashaPluginContextPack>();
builder.Services.AddPluginCore();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// 2. ʹ�� PluginCore
app.UsePluginCore();

//app.UseAuthorization();

app.MapControllers();

app.Run();
