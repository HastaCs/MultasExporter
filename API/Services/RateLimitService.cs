using System.Collections.Concurrent;

namespace API.Services
{
  public class RateLimitService
  {
    private readonly ConcurrentDictionary<string, List<DateTime>> _requestHistory = new();
    private readonly int _maxRequestsPerDay;

    public RateLimitService(int maxRequestsPerDay = 10)
    {
      _maxRequestsPerDay = maxRequestsPerDay;
    }

    public bool IsAllowed(string ipAddress)
    {
      var now = DateTime.UtcNow;
      var oneDayAgo = now.AddDays(-1);

      // Obtener o crear el historial de peticiones para esta IP
      var requests = _requestHistory.GetOrAdd(ipAddress, _ => new List<DateTime>());

      lock (requests)
      {
        // Limpiar peticiones antiguas (más de 24 horas)
        requests.RemoveAll(r => r < oneDayAgo);

        // Verificar si se ha alcanzado el límite
        if (requests.Count >= _maxRequestsPerDay)
        {
          return false;
        }

        // Registrar la nueva petición
        requests.Add(now);
        return true;
      }
    }

    public int GetRemainingRequests(string ipAddress)
    {
      var now = DateTime.UtcNow;
      var oneDayAgo = now.AddDays(-1);

      if (!_requestHistory.TryGetValue(ipAddress, out var requests))
      {
        return _maxRequestsPerDay;
      }

      lock (requests)
      {
        requests.RemoveAll(r => r < oneDayAgo);
        return Math.Max(0, _maxRequestsPerDay - requests.Count);
      }
    }
  }
}
