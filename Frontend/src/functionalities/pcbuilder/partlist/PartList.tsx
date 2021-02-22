import React from 'react';
import '../../../styles/global.scss';
import './PartList.scss';
import PartTile from "./parttile/PartTile";
import {IPart} from "../../../app/models/IPart";

const PartList: React.FC = () => {
    const motherboard: IPart = {
        title: 'Moederbord',
        description: 'Het moederbord is het beginsel van je computer. Hier sluit je alle onderdelen op aan.',
        image: 'https://tweakers.net/ext/i/2000645660.png',
        type: 'motherboard'
    }

    const processor: IPart = {
        title: 'Processor',
        description: 'De processor is het rekenkundige brein van je computer. Deze doet alle berekeningen van de ' +
            'computer.',
        image: 'https://content.hwigroup.net/images/articles/600px_cpu.png',
        type: 'processor'
    }

    const processorCooler: IPart = {
        title: 'Processorkoeler',
        description: 'De processor produceert veel hitte. De koeler is belangrijk, zodat je processor niet ' +
            'oververhit raakt.',
        image: 'https://cdn-reichelt.de/bilder/web/xxl_ws/E200/BEQUIET_BK021-01.png',
        type: 'processorcooler'
    }

    const ram: IPart = {
        title: 'Werkgeheugen',
        description: 'Op het werkgeheugen worden applicaties geladen als je ze gaat gebruiken. Belangrijk voor grote ' +
            'workloads.',
        image: 'https://www.corsair.com/medias/sys_master/images/images/h7a/h49/8988098330654/-CMK8GX4M1D3000C16-' +
            'Gallery-VENG-LPX-BLK-01.png',
        type: 'ram'
    }

    const Pcase: IPart = {
        title: 'Behuizing',
        description: 'Dit is de uiteindelijke kast waar alle onderdelen netjes in zullen worden opgeborgen.',
        image: 'https://nzxt-site-media.s3-us-west-2.amazonaws.com/assets/homepage/h510-elite-white-black' +
            '-dc7369e78f982928f808609aa9879b8d8e84ea9138ddbaeec97a066ab415de98.png',
        type: 'case'
    }

    const graphicsCard: IPart = {
        title: 'Grafische kaart',
        description: 'Dit is het grafische brein van de computer. Dit onderdeel is vooral belangrijk bij het gamen en' +
            ' bijvoorbeeld photoshop.',
        image: 'https://www.nvidia.com/content/dam/en-zz/Solutions/geforce/ampere/rtx-3080/geforce-rtx-' +
            '3080-shop-630-d@2x.png',
        type: 'graphicscard'
    }

    const powerSupply: IPart = {
        title: 'Voeding',
        description: 'Dit onderdeel zet stroom van de muur om in stroom die jouw computer kan gebruiken om te werken.',
        image: 'https://www.corsair.com/medias/sys_master/images/images/h18/hcd/9188997595166/-CP-9020171' +
            '-EU-Gallery-VS550-PSU-01.png',
        type: 'powersupply'
    }

    const storageDevice: IPart = {
        title: 'Opslagmedia',
        description: 'Dit zijn onderdelen waar je data permanent op wordt opgeslagen (harde schijven, etc.)',
        image: 'https://www.pcspecialist.nl/images/landing/seagate/ironwolf/seagate-ironwolf-hdd.png',
        type: 'storagedevice'
    }

    const operatingSystem: IPart = {
        title: 'Besturingssysteem',
        description: 'Dit is de software waar je computer op draait, denk hier bij aan Windows, Mac OS of Linux.',
        image: 'https://www.ervio.nl/wp-content/uploads/2019/10/windows_logos_PNG24.png',
        type: 'operatingsystem'
    }

    return (
        <div className="partlist">
            <PartTile part={motherboard}/>
            <PartTile part={processor}/>
            <PartTile part={processorCooler}/>
            <PartTile part={ram}/>
            <PartTile part={Pcase}/>
            <PartTile part={graphicsCard}/>
            <PartTile part={powerSupply}/>
            <PartTile part={storageDevice}/>
            <PartTile part={operatingSystem}/>
        </div>
    );
}

export default PartList;
