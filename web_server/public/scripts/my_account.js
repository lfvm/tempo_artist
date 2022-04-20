
const logOut = (e) => {
    
    e.preventDefault();
    //Borrar los datos guardaos 

    localStorage.removeItem('mail');
    localStorage.removeItem('nombre');
    localStorage.removeItem('apellidos');
    localStorage.removeItem('id');

    //navegar a inicio
    window.location.href = '/login';

}


const setInputValues = () => {

    const mail = localStorage.getItem('mail');
    const name = localStorage.getItem('name');
    const lastName = localStorage.getItem('last_name');

    document.getElementById('title').innerHTML = name + " " + lastName;
    document.getElementById('name_input').value = name;
    document.getElementById('last_input').value = lastName;
    document.getElementById('email_input').value = mail



}

const handleUpdateAccount = async(e) => {

    const name = document.getElementById('name_input').value 
    const last_name = document.getElementById('last_input').value 
    const mail = document.getElementById('email_input').value 
    const user_id = localStorage.getItem('id');

    const data = {
        name,
        last_name,
        mail,
    }

    //Hcer la peticion para actualizar los datos
    const response = await fetch(`/api/usuarios/${user_id}`, {
        method: 'PUT',
        body: JSON.stringify(data),
        headers: {
            'Content-Type': 'application/json'
        }
    }).then(res => res.json())
    .then(res => {
        console.log(res);
        if(res.status == 'succes'){

            //Actualizar los datos del local storage 
            localStorage.setItem('name', name);
            localStorage.setItem('last_name', last_name);
            localStorage.setItem('mail', mail);

            //Hacer refresh de la pagina
            location.reload();

        }
    });
}