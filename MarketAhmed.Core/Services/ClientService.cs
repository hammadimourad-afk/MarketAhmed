using MarketAhmed.Core.Interfaces;
using MarketAhmed.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MarketAhmed.Core.Services
{
    public class ClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public IEnumerable<Client> GetAllClients()
        {
            return _clientRepository.GetAll();
        }

        public Client GetClientById(int id)
        {
            return _clientRepository.GetById(id);
        }


        public bool AddClient(Client client)
        {
            // Exemple de logique métier: vérifier si l'email est unique
            if (_clientRepository.GetByEmail(client.Email) != null)
            {
                // L'email existe déjà, on ne peut pas ajouter le client
                return false;
            }

            // Validation simple des champs obligatoires
            if (string.IsNullOrWhiteSpace(client.Nom) || string.IsNullOrWhiteSpace(client.Prenom) || string.IsNullOrWhiteSpace(client.Email))
            {
                return false;
            }

            _clientRepository.Add(client);
            return true;
        }

        public bool UpdateClient(Client client)
        {
            // Vérifier si le client existe
            var existingClient = _clientRepository.GetById(client.IdClient);
            if (existingClient == null)
            {
                return false; // Client non trouvé
            }

            // Vérifier l'unicité de l'email si l'email a changé
            if (existingClient.Email != client.Email)
            {
                var clientWithSameEmail = _clientRepository.GetByEmail(client.Email);
                if (clientWithSameEmail != null && clientWithSameEmail.IdClient != client.IdClient)
                {
                    return false; // L'email est déjà utilisé par un autre client
                }
            }

            // Validation simple des champs obligatoires
            if (string.IsNullOrWhiteSpace(client.Nom) || string.IsNullOrWhiteSpace(client.Prenom) || string.IsNullOrWhiteSpace(client.Email))
            {
                return false;
            }

            _clientRepository.Update(client);
            return true;
        }

        public bool DeleteClient(int id)
        {
            // Exemple de logique métier: vérifier s'il y a des ventes associées avant de supprimer (à implémenter)
            // Pour l'instant, suppression directe
            var clientToDelete = _clientRepository.GetById(id);
            if (clientToDelete == null)
            {
                return false; // Client non trouvé
            }
            _clientRepository.Delete(id);
            return true;
        }
    }
}