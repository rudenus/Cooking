import { Editor } from 'primereact/editor';
import { useState } from 'react';
import { FileUpload } from 'primereact/fileupload';
import './Step.css';
const Step = ({steps, stepId, updateStepInParentState}) => {
    const header = (
    <div>
        <span className="ql-formats">
            <button className="ql-bold" aria-label="Bold"></button>
            <button className="ql-italic" aria-label="Italic"></button>
            <button className="ql-underline" aria-label="Underline"></button>
        </span>
    </div>
    );

    const [description, setDescription] = useState('');
    const [image, setImage] = useState(null)
    const [imageURL, setImageURL] = useState(null)

    function descriptionChangeHandler(value){
        let newStep ={
            stepId:stepId,
            description:value,
            file:image,
        }
        setDescription(value)
        updateStepInParentState(newStep)
    }

    const onSelect = (event) => {
        let files = event.originalEvent.target.files
        let stepNode = document.querySelector(`#${stepId}`)
        if (files && files[0]) {
            let newStep ={
                stepId:stepId,
                description:description,
                file:files[0],
            }    
            setImage(files[0])
            setImageURL(URL.createObjectURL(files[0]));
            updateStepInParentState(newStep)
            let previewImage = stepNode.querySelector(".preview-image")
            previewImage.style.display = '';
        }
    }

    function onClear(e){
        let newStep ={
            stepId:stepId,
            description:description,
            file:null,
        }    
        setImage(null)
        setImageURL(null)
        updateStepInParentState(newStep)

        let stepNode = document.querySelector(`#${stepId}`)
        let previewImage = stepNode.querySelector(".preview-image")
        previewImage.style.display = 'none';
    }
/* <div className='input-step-time'>
                <span style={{margin:'30px'}}>Укажите время, затраченное на этом шаге: </span>
                <input type="text" value={time} style={{padding:'3px 10px', minWidth:'300px'}} onInput={onTimeChange}/>
            </div>*/ 
    return (
        <div id={stepId} className="step">
            <Editor value={description}  onTextChange={(e) => descriptionChangeHandler(e.htmlValue)} headerTemplate={header} style={{ height: '220px' }} className='step-description'/>
           
            <div>
                <div style={{fontSize:'18px', textAlign:'left', margin:'40px 10px 10px 130px'}}>Вы можете приложить фото</div>
                <FileUpload mode="basic" chooseLabel='Добавить файл' onClear={onClear} onSelect={onSelect}  name="demo[]" accept="image/*" maxFileSize={1000000} style={{fontSize:'16px', margin:'30px 10px 10px 140px', textAlign:'left'}}/>
                <img alt="preview image" src={imageURL} style={{display:'none'}} className='preview-image'/>
            </div>
        </div>
        );
}

export default Step;