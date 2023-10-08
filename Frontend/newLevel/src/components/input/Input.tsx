
const Input = ({type, name, placeholder, handleOnChange}: any) => {
  return (
    <>
    <input type={type} name={name} id={name} placeholder={placeholder} onChange={handleOnChange} />
    </>
  )
}

export default Input