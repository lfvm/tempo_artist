const express = require('express');
let router = express.Router();
const connection = require('../../db/db_config')

//Archivo para manejar operaciones CRUD con las puntuaciones


//Ruta para obtener todos los usuarios
router.get('/', async(req, res) => {

    await connection.connect();


    connection.query(`SELECT * FROM USUARIOS`, (err, rows, fields) => {
        
        if (!err) {
            res.json({
                status: 'success', 
                rows
            });

        } else {

            res.json({
                status: 'fail', 
                'message': err
            });
        }
    });

    connection.end();


});


//Ruta para crear un usuario
router.post('/nuevo', async(req, res) => {

    res.json({
        status: 'success',
    })

});






module.exports = router;