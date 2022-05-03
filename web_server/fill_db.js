//Sript para llenar la base de datos de usuarios y scores utilizando 
//el paquete faker de nodejs
const { faker } = require('@faker-js/faker');
const axios = require('axios')



const genders = ["male", "female"]
const instrument = ["yes", "no"]
const levelIds = [4,14,24,34,44,54]

const getRandomElement =( data ) => {
    //Funcion que obtiene un elemento al azar de la lista
    return data[Math.floor(Math.random()*data.length)];

}

const CreateUser = () => {

    const user = {
        name: faker.name.firstName(),
        mail: faker.internet.email(),
        last_name: faker.name.lastName(),
        password: faker.internet.password(),
        gender: getRandomElement(genders),
        plays_instrument: getRandomElement(instrument),
        age: Math.floor(faker.datatype.number({ min: 10, max: 40,})) 
    }
    return user;
}


const CreateScore = ( userId ) => {
    const score = {

        user_id: userId,
        level_id: getRandomElement(levelIds),
        total_points: Math.floor(faker.datatype.number({ min: 300, max: 5000,})),
        perfect_hits: Math.floor(faker.datatype.number({ min: 0, max: 300,})),
        good_hits: Math.floor(faker.datatype.number({ min: 50, max: 300,})),
        accuracy: Math.floor(faker.datatype.number({ min: 50, max: 100,})),
        max_combo: Math.floor(faker.datatype.number({ min: 1000, max: 7000,})),
    }
    return score;
}



const fillDb = async ( ) => {

    //Funcion que crea 10 usuarios y agrega 10 scores por cada uno
    for(let i = 0; i < 10; i++){

        const user = CreateUser();

        //Hacer el fecth a la api para crear el usuario usando el paquete
       //axios
        const response = await axios.post('http://localhost:8080/api/usuarios/nuevo', user);
        

        if (response.status === 200){
            const userId = response.data.id;
            for(let j = 0; j < 10; j++){
                const score = CreateScore(userId);
                const scoreResponse = await axios.post('http://localhost:8080/api/puntuaciones/nueva', score);
                console.log(scoreResponse['data']);
            }
        }




    }
}


fillDb();