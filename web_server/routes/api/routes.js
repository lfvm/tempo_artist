const express = require('express');
let router = express.Router();
const connection = require('../../db/db_config')


router.get('/', (req, res) => {

    res.json({
        message: 'Api get',
    });
});

router.post('/:id', (req, res) => {

    const id = req.params.id;
    res.json({
        message: 'Api post:' + id
    });
});

router.put('/:id', (req, res) => {

    const id = req.params.id;
    res.json({
        message: 'Api put:' + id
    });
});

router.delete('/:id', (req, res) => {

    const id = req.params.id;
    res.json({
        message: 'Api delete:' + id
    });
});


module.exports = router;