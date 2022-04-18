
const handleLogin = async(e) => {

    //Funcion para hacer login e ir a home

    e.preventDefault();

    //Obtener los datos del form
    const loginForm = document.getElementById('login_form');
    const email = loginForm[0].value;
    const password = loginForm[1].value;

    //Verificar que los datos no esten vacios, de lo contrario mostrar mensaje de error
    if (email === '' || password === '') {
        redHighlights(email, password);
        addAlert("Please fill in all the required fields.");
        
    } else {
       
        const data = {correo : email, password}

        //Mandar request a la API
        fetch('http://localhost:8080/api/usuarios/login', {
            method: "POST",
            body: JSON.stringify(data),
            headers: {"Content-type": "application/json; charset=UTF-8"}
        })
        .then( res => res.json())
        .then(response => {
            
    
            if(response['status'] === 'succes'){
            
                //Guardar los datos en el localStorage en caso de que el login sea exitoso
                localStorage.setItem('mail', email);
                localStorage.setItem('nombre', response['user']['nombre']);
                localStorage.setItem('apellidos', response['user']['apellidos']);
                localStorage.setItem('id', response['user']['id_usaurio']);


                //Redirigir a home
                window.location.href = '/';
    
            } else {
                
                //Mostrar mensajes de error
                addAlert(response['msg']);
                
            }

        })
        .catch(err => console.log(err));
    }
}



const handleCreateAccount = async(e) => {

    e.preventDefault();

    const form = document.getElementById('create_account_form');

    //Obtener los datos del formulario
    const correo = form[0].value;
    const password = form[1].value;
    const nombre = form[2].value;
    const apellidos = form[3].value;
    const gender = form[4].value;
    const instrumento = form[5].value;
    const edad = form[6].value;
    const data = {
        correo,
        password,        
        nombre,
        apellidos,
        gender,
        instrumento,
        edad
    }


    //verificar que los datos del objeto data no esten vacios
    for (const element in data) {

        if (data[element] === ''){

            addAlert("Please fill in all the required fields.");
            return
        }
    }

    //Hacer llamada a la API
    fetch('http://localhost:8080/api/usuarios/nuevo', {
        method: "POST",
        body: JSON.stringify(data),
        headers: {"Content-type": "application/json; charset=UTF-8"}
    })
        .then( res => res.json())
        .then(response => {
            
            console.log(response);

            if(response['status'] === 'succes'){
            
                localStorage.setItem('mail',form[0].value);
                localStorage.setItem('nombre', response['user']['nombre']);
                localStorage.setItem('apellidos', response['user']['apellidos']);
                localStorage.setItem('id', response['id']);
                window.location.href = '/';
    
            } else {

                //Verificar si el error es por correo ya existente
                if(response['err']['code'] === 'ER_DUP_ENTRY'){
                    addAlert("The email is already in use.");
                } else {
                    addAlert("There was an error while creating your account, please try again.");
                }
            }

        })
        .catch(err => console.log(err));
}


const type_effect = async(e) => {
    let i = 0;
    let placeholder_1 = "";
    let placeholder_2 = "";
    const txt_1 = "example@mail.com";
    const txt_2 = "password";
    const speed = 150;

    function effect(){
        placeholder_1 += txt_1.charAt(i);
        placeholder_2 += txt_2.charAt(i);
        document.getElementById("exampleInputEmail1").setAttribute("placeholder", placeholder_1);
        document.getElementById("exampleInputPassword1").setAttribute("placeholder", placeholder_2);
        i++;
        setTimeout(effect, speed);
    } 
    effect();
}



function addAlert(text){

    if (alertInScreen()){
        const pathname = window.location.pathname
    
        let form;
    
        if (pathname == '/create'){
            form = document.getElementById("create_account_form");
        } else{
            form = document.getElementById("login_form");
        }
        
    
        div = document.createElement("div");
    
        div.setAttribute("class", "alert alert-danger alert-dismissible fade show");
        div.setAttribute("role", "alert");
        div.appendChild(document.createTextNode(text))
    
        form.appendChild(div)
    }
    else {
        shakeAlert();
    }

}



function redHighlights(email, password){
    // TODO: Limpiar este codigo
    let email_input = document.getElementById("exampleInputEmail1");
    let password_input = document.getElementById("exampleInputPassword1");

    let email_label = document.querySelector("label[for=exampleInputEmail1]");
    let password_label = document.querySelector("label[for=exampleInputPassword1]");


    email_input.setAttribute("class", "form-control");
    password_input.setAttribute("class", "form-control");
    email_label.classList.remove("class", "set-red-label");
    password_label.classList.remove("class", "set-red-label");

    if (email === ''){
        email_input.classList.add("class", "is-invalid")
        email_label.classList.add("class", "set-red-label")
    }
    if (password === ''){
        password_input.classList.add("class", "is-invalid");
        password_label.classList.add("class", "set-red-label")
    }
}



function alertInScreen(){

    const pathname = window.location.pathname

    if (pathname === '/login'){
        let form = document.getElementById('login_form');
        return form.lastElementChild.className == "login_btn";
    } else {
        let form = document.getElementById('create_account_form');
        return form.lastElementChild.className == "login_btn";
    }
}

function shakeAlert(){
    let alert = document.getElementsByClassName("alert alert-danger alert-dismissible fade show");

    alert[0].classList.add("apply-shake")
    
    // Remover la animacion para que aparezca cada que se de click
    alert[0].addEventListener("animationend", (e) => {
        alert[0].classList.remove("apply-shake");
    }); 
}

function main(){
    type_effect()
}

