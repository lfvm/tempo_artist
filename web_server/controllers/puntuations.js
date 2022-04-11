const connection = require("../db/db_config")


const getAllPunctuations = (req, res) => {

    connection.query(`SELECT * FROM puntuaciones`, (err, rows, fields) => {
        
        if (!err) {

            punctuations = rows;

            res.json({
                status: 'success', 
                punctuations
            });

        } else {

            res.json({
                status: 'fail', 
                err
            });
        }
    });

}











module.exports = {

    getAllPunctuations,
}