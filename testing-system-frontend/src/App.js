import Header from './template/header.js'
import CreateQuestion from './components/createquestion/CreateQuestion'
import CreateTestForm from './components/CreateTestForm'
import CreateGroupForm from './components/CreateGroupForm'

function App() {
  return (
    <div className="App">
      <Header></Header>
      <CreateQuestion></CreateQuestion>
      <CreateGroupForm />
      <CreateTestForm></CreateTestForm>
    </div>
  );
}

export default App;
