import { useState } from "react"
import { IRegisterUser } from "../../interfaces/IRegisterUser"
import { useLoading } from "../../context/LoadingContext"
import { useAlert } from "../../context/PopupContext"
import Input from "../input/Input"
import Style from './FormRegister.module.css'
import axiosInstance from "../../axiosInstances"
import PopupMessages from "../popupMessages/PopupMessages"

const FormRegister = () => {
  const[name, setName] = useState<string>()
  const[login, setLogin] = useState<string>()
  const[email, setEmail] = useState<string>()
  const[password, setPassword] = useState<string>()
  const[confirmPassword, setconfirmPassword] = useState<string>()
  const { setIsLoading } = useLoading()
  const {showAlert} = useAlert()

  const submit = async () => {
    try {
      if(email === '' || name === '' || login === '' || password === '' || confirmPassword === '') {
        showAlert('warning' , 'Preencha os campos')
      } else {
        setIsLoading(true)
      }
  
      const userToRegister: IRegisterUser = {
        email: email,
        name: name,
        login: login,
        password: password,
        confirmPassword: confirmPassword,
        birthplace: '',
      }
  
      const resp = await axiosInstance.post('/Auth/Register', userToRegister)
      
      showAlert('success', resp.data.message)
      
    } catch (err: any) {
      if (err.code === 'ERR_NETWORK') {
        showAlert('error', err.message)
      } else {
        showAlert('error', err.response.data.message);
      }
      
    } finally {
      setIsLoading(false)
    }
  }

  return (
    <>
      <form action="#">
          <h1>Registre-se</h1>
          <span>use seu email para o registro</span>
          <Input type='text' id='name' placeholder='Nome' handleOnChange={(e:any) => setName(e.target.value)}/>
          <Input type='text' id='login' placeholder='Login' handleOnChange={(e:any) => setLogin(e.target.value)}/>
          <Input type='email' id='email' placeholder='Email' handleOnChange={(e:any) => setEmail(e.target.value)}/>
          <Input type='password' id='password' placeholder='Senha' handleOnChange={(e:any) => setPassword(e.target.value)}/>
          <Input type='password' id='confirmPassword' placeholder='Confirme a senha' handleOnChange={(e:any) => setconfirmPassword(e.target.value)}/>
          <button className={Style.hover_button} onClick={submit}>Registrar</button>
          <span className={Style.span_obs}>*Você poderá logar tanto por email ou login</span>
          <span className={Style.span_obs}>*Nome será utilizado dentro da plataforma para os outros usuários te reconhecerem</span>
      </form>
    </>
  )
}

export default FormRegister