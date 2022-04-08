
/*
  Scipt para conectar con la base de datos mysql
*/

var mysql = require('mysql');
var connection = mysql.createConnection({
  host :"us-cdbr-east-05.cleardb.net",
  user: "bdc4a16bdf0c22",
  password: "ff9c831b",
  database: "heroku_54233b1e776c413"
});

connection.connect();


module.exports = connection;


