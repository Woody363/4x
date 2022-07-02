
using Npgsql;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


 public  class Test{
        public  bool CheckPermission()
{

var cs = "Host=127.0.0.1:5431;Username=postgres;Password=password;Database=postgres";
using var con = new NpgsqlConnection(cs);
con.Open();

var sql = "SELECT version()";

using var cmd = new NpgsqlCommand(sql, con);

var version = cmd.ExecuteScalar().ToString();
//using new context(){}
Console.WriteLine($"PostgreSQL version: {version}");


try{

/*

using(db2.context){
    return db2.hasPermission where 


}


*/

}catch{
//throw error email

}
return true;
}

}