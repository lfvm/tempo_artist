
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
    const name = localStorage.getItem('nombre');
    const lastName = localStorage.getItem('apellidos');

    document.getElementById('title').innerHTML = name + " " + lastName;
    document.getElementById('name_input').value = name;
    document.getElementById('last_input').value = lastName;
    document.getElementById('email_input').value = mail



}

const handleUpdateAccount = async(e) => {

    const nombre = document.getElementById('name_input').value 
    const apellidos = document.getElementById('last_input').value 
    const correo = document.getElementById('email_input').value 
    const id = localStorage.getItem('id');

    const data = {
        nombre,
        apellidos,
        correo
    }

    //Hcer la peticion para actualizar los datos
    const response = await fetch(`/api/usuarios/${id}`, {
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
            localStorage.setItem('nombre', nombre);
            localStorage.setItem('apellidos', apellidos);
            localStorage.setItem('mail', correo);
            alert(res['msg']);

            //Hacer refresh de la pagina
            location.reload();

        }
    });
}