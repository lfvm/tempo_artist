/*
    Un middleware es una funcion que se ejcuta antes de realizar
    la funcion principal de una ruta,
    como verificar que los campos necesarios esten en el reuqest y 
    que estos sean correctos.
    De lo contrario se manda una respuesta de eror
*/
const { req,res  } = require('express');
const { validationResult } = require('express-validator');


const validateRequestFields = ( req, res, next ) => {

    const errors = validationResult(req);
    if ( !errors.isEmpty() ){
        return res.status(400).json(errors);
    }

    //Continuar con la siguiente funcion
    next();

}



module.exports = {
    validateRequestFields,
}