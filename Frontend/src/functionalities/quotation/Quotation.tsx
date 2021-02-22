import React from 'react';
import './Quotation.scss';
import {Form, Field} from 'react-final-form'

const Quotation: React.FC = () => {

    const handleSubmit = () => {
        alert("Uw offerte word verstuurd!")
    }

    return (
        <div className="quotation">
            <h2>Offerte versturen</h2>
            <p>Vul alstublieft uw informatie in, zodat wij u kunnen contacteren wanneer wij reageren op de offerte</p>
            <Form
                onSubmit={handleSubmit}
                render={({ handleSubmit }) => (
                    <form onSubmit={handleSubmit}>
                        <div>
                            <label>Volledige naam</label>
                            <Field name="fullName" component="input" placeholder="Naam" />
                        </div>
                        <div>
                            <label>Email</label>
                            <Field name="email" component="input" placeholder="example@example.com" />
                        </div>
                        <div>
                            <label>Postcode</label>
                            <Field name="zipcode" component="input" placeholder="Postcode" />
                        </div>
                        <div>
                            <label>Huisnummer</label>
                            <Field name="housenumber" component="input" placeholder="Huisnummer" />
                        </div>
                        <div>
                            <label>Telefoonnummer</label>
                            <Field name="phone" component="input" placeholder="Telefoonnummer" />
                        </div>
                        <div>
                            <label>Toevoegingen</label>
                            <Field name="extra" component="textarea"
                                   placeholder="Voeg hier eventuele opmerkingen of informatie toe..." />
                        </div>
                        <button type="submit">Versturen</button>
                    </form>
                )}
            />
        </div>
    );
}

export default Quotation;
