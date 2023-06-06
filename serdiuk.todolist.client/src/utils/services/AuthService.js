import { ACCOUNT_URL } from "../Constants";

async function postData(url, data={}) {
    var responce = await fetch(`${ACCOUNT_URL}/${url}`, {
        method: "POST",
        headers:{
            "Content-Type": "application/json",           
        },
        body: JSON.stringify(data)
    })
    return responce;
}

export async function login(email, password){
    var json = await postData("login", {email,password})
    .then(res=>{window.location.href='/'; return res.json()});
    await setToken(json.accessToken);
}
export async function register(email, password, confirmPassword){
    var json = await postData("register", {email,password, confirmPassword})
    .then(res=>{window.location.href='/'; return res.json()});
    await setToken(json.accessToken);
}

async function setToken(token){
    await localStorage.removeItem("token");
    await localStorage.setItem("token", token);

}