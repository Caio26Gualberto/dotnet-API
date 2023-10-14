import Style from './ForgotPassword.module.css'
import '../../styles.css'

const ForgotPassword = () => {
  return (
    <>
    <div className={Style.background_gif}></div>
    <div className="container mx-auto container-form">
        <div className="flex justify-center items-center h-screen px-6">
            <div className="w-full xl:w-3/4 lg:w-11/12 flex">
                <div className={`w-full h-auto bg-gray-400 hidden lg:block lg:w-1/2 bg-cover rounded-l-lg ${Style.background_image}`}></div>
                <div className="w-full lg:w-1/2 bg-white p-5 rounded-lg lg:rounded-l-none">
                    <div className="px-8 mb-4 text-center">
                        <h3 className="pt-4 mb-2 text-2xl">Esqueceu sua senha?</h3>
                        <p className="mb-4 text-sm text-gray-700">
                            Você poderá trocar de senha aqui
                        </p>
                    </div>
                    <form className="px-8 pt-6 pb-8 mb-4 bg-white rounded">
                        <div className="mb-4">
                            <label className="block mb-2 text-sm font-bold text-gray-700" htmlFor="email">
                                Senha
                            </label>
                            <input
                                className="w-full px-3 py-2 text-sm leading-tight text-gray-700 border rounded shadow appearance-none focus:outline-none focus:shadow-outline"
                                id="password" type="password" placeholder="Nova senha" />
                            <input className={`w-full px-3 py-2 text-sm leading-tight text-gray-700 border rounded shadow appearance-none focus:outline-none focus:shadow-outline ${Style.margin}`}
                                id="newPassword" type="password" placeholder="Confirme a senha" />
                                <div>
                                    <label className={Style.switch}>
                                      <input type="checkbox" //</label>onclick="showPassword()"
                                      ></input>
                                      <span className={Style.slider}></span>
                                    </label> Exibir senha
                                  </div>
                        </div>
                        <div className="mb-6 text-center">
                            <button
                                className="w-full px-4 py-2 font-bold text-white bg-red-500 rounded-full hover:bg-red-700 focus:outline-none focus:shadow-outline"
                                type="button" //onClick="updatePassword()"
                                >
                                Confirmar
                            </button>
                        </div>
                        <hr className="mb-6 border-t" />
                        <div id="newPasswordMessage"></div>
                        <div className={Style.linkRedirect} id="linkRedirect"></div>
                    </form>
                </div>
            </div>
        </div>
    </div>
    </>
  )
}

export default ForgotPassword