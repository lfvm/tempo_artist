
//Importar express y crear una app cone l
const { request } = require('express');
const express = require('express');
const cors = require('cors');
const users = require('./routes/api/users');
const levels = require('./routes/api/levels');
const scores = require('./routes/api/scores');
const pages = require('./routes/screens/routes');
const { check } = require('express-validator');

const app = express();

//Definir puerto en donde correra la app
PORT = 8080;



//Definir que nuestra app utilizara json para las respuestas
app.use(express.json())
app.use(cors());
//Defiinir el folder donde se eucnentra el html y css del proyecto
app.use( express.static('public') );




//Definiir rutas de la api
app.use('/api/usuarios', users);
app.use('/api/puntuaciones', scores);
app.use('/api/niveles', levels);



//paginas que se muestran al usar la app;
app.use('/',[
    check('id_usuario').not().isEmpty(),
], pages);



app.listen( PORT, () => {
    console.log(`App corriendo en http://localhost:${PORT}`)
})

