using AutoMapper;
using ClientsApi.Data;
using ClientsApi.Models;
using ClientsApi.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using System.Collections.Generic;
using System;

namespace ClientsApi.Controllers;

[ApiController]
[Route("Controller")]
public class ClientEFController : ControllerBase
{
    IClientRepository _clientRepository;
    IMapper _mapper;

    public ClientEFController(IConfiguration config , IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
        _mapper = new Mapper(new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<ClientToAddDto, Client>();
        }));
    }

    [HttpGet("GetUsers")]
    public IEnumerable<Client> GetClients()
    {
        IEnumerable<Client> clients = _clientRepository.GetClients();
        return clients;
    }

    [HttpGet("GetSingleClient")]
    public Client GetSingleClient(int clientId)
    {
        return _clientRepository.GetSingleClient(clientId);
    }

    [HttpPut("EditClient")]
    public IActionResult EditClient(Client client)
    {
        Client? clientdb = _clientRepository.GetSingleClient(client.ClientId);

        if(clientdb != null)
        {
            clientdb.Name = client.Name;
            clientdb.LastName = client.LastName;
            clientdb.Age = client.Age;
            clientdb.DNI = client.DNI;

            if(_clientRepository.SaveChanges())
            {
                return Ok();
            }
        }

        throw new System.Exception("Error al actualizar el cliente");
    }

    [HttpPost("AddUser")]
    public IActionResult AddUser(ClientToAddDto client)
    {
        Client clientDb = _mapper.Map<Client>(client);

        _clientRepository.AddEntity<Client>(clientDb);
        if(_clientRepository.SaveChanges())
        {
            return Ok();
        }

        throw new Exception("Error al añadir un cliente");
    }

    [HttpDelete("DeletelUser/{clientId}")]
    public IActionResult DeleteUser(int clientId)
    {
        Client ? clientDb = _clientRepository.GetSingleClient(clientId);

        if (clientDb != null)
        {
            _clientRepository.RemoveEntity<Client>(clientDb);
            if (_clientRepository.SaveChanges())
            {
                return Ok();
            }
        }

        throw new Exception("Error al borrar usuario");
    }
}