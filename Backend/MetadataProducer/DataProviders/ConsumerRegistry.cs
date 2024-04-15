namespace TV2.Backend.Services.MetadataProducer.DataProviders;

using ClassLibrary.Classes;
using Interfaces;
using MySql.Data.MySqlClient;

public class ConsumerRegistry : IConsumerRegistry
{
    private string cs = @"server=producer.metadata.database;userid=root;password=;database=db";
    public void setConnectionString(string connectionString) => cs = connectionString;
    private readonly ILogger<ConsumerRegistry>? _logger;
    
    public ConsumerRegistry(ILogger<ConsumerRegistry>? logger = null)
    {
        _logger = logger;
    }

    private bool Open(MySqlConnection con)
    {
        try
        {
            con.Open();
            return true;
        }
        catch (Exception err)
        {
            _logger?.LogError(err, "Failed to connect to DB");
            return false;
        }
    }
    
    public bool Create(string name)
    {
        using var con = new MySqlConnection(cs);
        if (!Open(con))
            return false;

        var host = new MetadataHost(Guid.NewGuid(), name); 
        
        var sql = @"INSERT INTO Host(ID, Name) VALUES(@id, @name)";
        using var cmd = new MySqlCommand(sql, con);
        
        cmd.Parameters.AddWithValue("@id", host.Id);
        cmd.Parameters.AddWithValue("@name", host.Name);
        cmd.Prepare();

        var result = cmd.ExecuteNonQuery();
        return result > 0;
    }

    public bool Update(MetadataHost host)
    {
        using var con = new MySqlConnection(cs);
        if (!Open(con))
            return false;
        
        var sql = "UPDATE Host SET Host.Name = @name WHERE Host.ID = @id";
        using var cmd = new MySqlCommand(sql, con);
        
        cmd.Parameters.AddWithValue("@id", host.Id);
        cmd.Parameters.AddWithValue("@name", host.Name);
        cmd.Prepare();

        var result = cmd.ExecuteNonQuery();
        return result > 0;
    }

    public bool Delete(MetadataHost host)
    {
        using var con = new MySqlConnection(cs);
        if (!Open(con))
            return false;
        
        var sql = "DELETE FROM Host WHERE Host.ID = @id";
        using var cmd = new MySqlCommand(sql, con);
        
        cmd.Parameters.AddWithValue("@id", host.Id);
        cmd.Prepare();
        
        var result = cmd.ExecuteNonQuery();
        return result > 0;
    }

    public IEnumerable<MetadataHost> List()
    {
        using var con = new MySqlConnection(cs);
        if (!Open(con))
            return new List<MetadataHost>();
        
        var sql = "SELECT * FROM Host";
        using var cmd = new MySqlCommand(sql, con);
        
        cmd.Prepare();
        
        using MySqlDataReader rdr = cmd.ExecuteReader();
        
        var hosts = new List<MetadataHost>();
        while (rdr.Read())
        {
            var host = new MetadataHost(
                rdr.GetGuid(0),
                rdr.GetString(1)
            );
            hosts.Add(host);
        }
        return hosts;
    }

    public MetadataHost? Resolve(Guid id)
    {
        using var con = new MySqlConnection(cs);
        if (!Open(con))
            return null;
        
        var sql = "SELECT * FROM Host WHERE Host.ID = @id";
        using var cmd = new MySqlCommand(sql, con);
        
        cmd.Parameters.AddWithValue("@id", id);
        cmd.Prepare();

        using MySqlDataReader rdr = cmd.ExecuteReader();

        if (rdr.Read())
        {
            var host = new MetadataHost(
                rdr.GetGuid(0),
                rdr.GetString(1)
            );
            return host;
        }

        return null;
    }
}