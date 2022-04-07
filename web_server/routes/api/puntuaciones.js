const express = require('express');
let router = express.Router();
const connection = require('../../db/db_config')


//Archivo para manejar operaciones CRUD con los usuarios



//Ruta para obtener todas las puntuaciones
router.get('/', async(req, res) => {

    await connection.connect();


    connection.query(`SELECT * FROM puntuaciones`, (err, rows, fields) => {
        
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




module.exports = router;