using ClientsApi.Data;
using ClientsApi.Models;
using ClientsApi.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System;
using System.Configuration;
using System.Collections.Generic;

namespace ClientsApi.Controllers;
[ApiController]
[Route("[controller]")] //este va a ser parte de la url antes del endPoint

public class ClientController : ControllerBase
{
    DataContextDapper _dapper;
    public ClientController(IConfiguration config)
    {
        _dapper = new DataContextDapper(config);
    }

    [HttpGet("TestConnection")]
    public DateTime TestConnection()
    {
        return _dapper.LoadDataSingle<DateTime>("SELECT  GETDATE()");
    }

    [HttpGet("GetClients")]
    public IEnumerable<Client> GetClients()
    {
        string sql = @"
            SELECT  
                [Name],
                [LastName],
                [Age],
                [DNI]
            FROM MyFirstAPIschema.Clients
        ";

        IEnumerable<Client> clients = _dapper.LoadData<Client>(sql);
        
        return clients;
    }

    [HttpGet("GetSingleUser/{clientId}")]
    public Client GetSingleUser(int clientId)
    {
        string sql = @"
        SELECT  
            [Name],
            [LastName],
            [Age],
            [DNI]
        FROM MyFirstAPIschema.Clients WHERE ClientId = " + clientId.ToString() ;
        Console.WriteLine(sql);
        Client client = _dapper.LoadDataSingle<Client>(sql);
        return client;
    }

    [HttpPut("EditClient")]
    public IActionResult EditClient(Client client)
    {
        string sql = @"UPDATE MyFirstAPIschema.Clients
            SET [Name] = '" + client.Name +
            "', [LastName] = '" + client.LastName +
            "', [Age] = '" + client.Age +
            "', [DNI] = '" + client.DNI +
        "'  WHERE ClientId = " + client.ClientId;

        Console.WriteLine(sql);
        if(_dapper.ExecuteSql(sql))
        {
            return Ok();
        }

        throw new Exception("Error al editar el cliente");
    }

    [HttpPost("AddClient")]
    public IActionResult AddClient(ClientToAddDto client)
    {
        string sql = @"
        INSERT MyFirstAPIschema.Clients
        (
            [Name],
            [LastName],
            [Age],
            [DNI]
        )VALUES('" + client.Name +
            "','" + client.LastName +
            "','" + client.Age +
            "','" + client.DNI +
        "')";

        //Console.WriteLine(sql);

        if(_dapper.ExecuteSql(sql)) 
        {
            return Ok();
        }

        throw new Exception("Error al crear el cliente en ddbb");
    }

    [HttpDelete("DeleteClient")]
    public string DeleteClient(int clientId)
    {
        string sql = "DELETE FROM MyFirstAPIschema.Clients WHERE ClientId = " + clientId.ToString();
        Console.WriteLine(sql);

        if(_dapper.ExecuteSql(sql))
        {
            return "cliente Borrado successfully";
        }

        throw new Exception("Falló al eliminar un el cliente" + clientId + "de la base de datos");
    }
}