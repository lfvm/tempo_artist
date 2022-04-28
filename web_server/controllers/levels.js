const connection = require("../db/db_config")


const getAllLevels = (req, res) => {

    connection.query("SELECT * FROM Levels", (err, rows, fields) => {
        if (!err) {
            
            res.json({
                status: "success",
                niveles: rows
            });

        } else {

            res.json({
                status: "fail",
                msg: err
            });
        }
    });
}



const createLevel = (req, res) => {

    const level = req.body;

    const query = `
    INSERT INTO Levels (
    name,
    difficulty,
    total_notes,
    author,
    level_type
    )VALUES(
        '${level.name}',
        '${level.difficulty}',
        '${level.total_notes}',
        '${level.author}',
        '${level.level_type}'
    );
    `


    connection.query(query, (err, rows, fields) => {

        if (!err) {
            
            res.json({
                status: "success",
                nivel: level,
                id: rows.insertId
            });

        } else {

            res.json({
                status: "fail",
                msg: err
            });
        }

    });
    
}

const updateLevelById = (req, res) => {

    const level = req.body;
    const id = req.params.id;
    const query = `
    UPDATE Levels SET
    name = '${level.name}',
    difficulty = '${level.difficulty}',
    total_notes = '${level.total_notes}',
    author = '${level.author}',
    level_type = '${level.level_type}'
    WHERE level_id = ${id}
    `


    connection.query(query, (err, rows, fields) => {

        if (!err) {
            
            res.json({
                status: "success",
                nivel: level,
                id: rows.insertId
            });

        } else {

            res.json({
                status: "fail",
                msg: err
            });
        }

    });
    
}



module.exports ={

    getAllLevels,
    createLevel,
    updateLevelById

}