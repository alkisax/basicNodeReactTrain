// frontend\src\pages\HomeBootstrap.tsx
import { useNavigate } from 'react-router-dom'

const HomeBootstrap = () => {
  const navigate = useNavigate()

  return (
    <div
      className='
        d-flex
        flex-column
        justify-content-center
        align-items-center
        vh-100
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
        <button
          onClick={() => navigate('/bootstrap-todo')}
          className='btn btn-primary'
        >
          go to todo page
        </button>
      </div>

      <button
        onClick={() => navigate('/')}
        className='btn btn-link btn-sm mt-3'
      >
        go to MUI mirror
      </button>
    </div>
  )
}

export default HomeBootstrap