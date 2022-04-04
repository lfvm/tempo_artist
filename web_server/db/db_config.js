
/*
  Scipt para conectar con la base de datos mysql
*/

var mysql = require('mysql');
var connection = mysql.createConnection({
  host :"us-cdbr-east-03.cleardb.com",
  user: "b64c03ae0a0611",
  password: "ddc2b455",
  database: "heroku_1a3d473a4d95416"
});


module.exports = connection;


