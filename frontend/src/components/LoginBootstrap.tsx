// frontend\src\components\LoginBootstrap.tsx

interface Props {
  handleUsernameSubmit: (e: React.SyntheticEvent) => void;
  usernameInput: string;
  setUsernameInput: React.Dispatch<React.SetStateAction<string>>;
}

const LoginBootstrap = ({ handleUsernameSubmit, usernameInput, setUsernameInput }: Props) => {
  return (
    <div
      className='
      d-flex
      justify-content-center
      align-items-center
      min-vh-100
    '
    >
      <div
        className='
        card
        d-flex
        justify-content-center
        align-items-center
        shadow-lg
      '
        style={{
          width: '300px',
          height: '200px',
          backgroundColor: '#1e1e1e',
          color: '#fff',
          borderRadius: '16px',
        }}
      >
        <form onSubmit={handleUsernameSubmit}>

          <div className='d-flex gap-2 align-items-center'>

            <input
              value={usernameInput}
              onChange={(e) => setUsernameInput(e.target.value)}
              type='text'
              placeholder='Username'
              className='form-control bg-dark text-light border-secondary'
            />

            <button
              type='submit'
              className='btn btn-success'
            >
              ✔️
            </button>

          </div>

        </form>
      </div>
    </div>
  )
}

export default LoginBootstrap