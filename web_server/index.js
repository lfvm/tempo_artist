
//Importar express y crear una app cone l
const { request } = require('express');
const express = require('express');
const cors = require('cors');
const app = express();
const Apiroutes = require('./routes/api/routes');
const pages = require('./routes/screens/routes');


//Definir puerto en donde correra la app
PORT = 8080;



//Definir que nuestra app utilizara json para las respuestas
app.use(express.json())
app.use(cors());
//Defiinir el folder donde se eucnentra el html y css del proyecto
app.use( express.static('public') );




//Definiir rutas

//paginas que se muestran al usar la app
app.use('/', pages);

//rutas para utilizar la api
app.use('/api', Apiroutes);



app.listen( PORT, () => {
    console.log(`App corriendo en http://localhost:${PORT}`)
})