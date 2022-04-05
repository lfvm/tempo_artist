const express = require('express');

let router = express.Router();


//Ruta principal de la aplicacion
router.get('/', (req, res) => {

    //Mandar un html que se encuentra en la carpeta public
    res.sendFile('index.html', { root: ( './public/') });
});



router.get('/login', (req, res) => {

    //Mandar un html que se encuentra en la carpeta public
    res.sendFile('login.html', { root: ( './public/') });
});


// Definir una ruta "comodin" por si se accede a una ruta inexistente
//Para ello se utiliza el simbolo *
router.get('*'  , (req,res) => {
    res.sendFile('not_found.html', { root: ( './public/') });

});




module.exports = router;