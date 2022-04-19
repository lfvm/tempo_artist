const express = require('express');

let router = express.Router();


//Ruta principal de la aplicacion
router.get('/', (req, res) => {

    //Mandar un html que se encuentra en la carpeta public
    res.sendFile('play.html', { root: ( './public/templates') });
});




 

router.get('/login', (req, res) => {

    //Mandar un html que se encuentra en la carpeta public
    res.sendFile('login.html', { root: ( './public/templates') });
});


router.get('/create', (req, res) => {

    //Mandar un html que se encuentra en la carpeta public
    res.sendFile('create_account.html', { root: ( './public/templates') });
});



router.get('/stats', (req, res) => {

    //Mandar un html que se encuentra en la carpeta public
    res.sendFile('my_stats.html', { root: ( './public/templates') });
});


router.get('/account', (req, res) => {

    //Mandar un html que se encuentra en la carpeta public
    res.sendFile('my_account.html', { root: ( './public/templates') });
});


// Definir una ruta "comodin" por si se accede a una ruta inexistente
//Para ello se utiliza el simbolo *
router.get('*'  , (req,res) => {
    res.sendFile('not_found.html', { root: ( './public/templates') });

});




module.exports = router;