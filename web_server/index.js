
//Importar express y crear una app cone l
const { request } = require('express');
const express = require('express');
const cors = require('cors');
const app = express();
const users = require('./routes/api/users');
const punctuations = require('./routes/api/puntuaciones');

const pages = require('./routes/screens/routes');


//Definir puerto en donde correra la app
PORT = 8080;



//Definir que nuestra app utilizara json para las respuestas
app.use(express.json())
app.use(cors());
//Defiinir el folder donde se eucnentra el html y css del proyecto
app.use( express.static('public') );




//Definiir rutas de la api
app.use('/api/usuarios', users);
app.use('/api/puntuaciones', punctuations);


//paginas que se muestran al usar la app;
app.use('/', pages);



app.listen( PORT, () => {
    console.log(`App corriendo en http://localhost:${PORT}`)
})

