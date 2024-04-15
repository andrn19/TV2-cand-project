type TopicInstance = {
    adjustedStart: string;
    adjustedEnd: string;
    start: string;
    end: string;
}

export type Topic = {
    id: number;
    name: string;
    referenceId: string;
    fullName: string;
    referenceType: string;
    iptcName: string;
    confidence: number;
    iabName: string;
    language: string;
    instances: TopicInstance[];
}

type LabelInstance = {
    confidence: number;
    adjustedStart: string;
    adjustedEnd: string;
    start: string;
    end: string;
}

export type Label = {
    id: number;
    name: string;
    referenceId: string;
    language: string;
    instances: LabelInstance[];
}

type KeywordInstance = {
    adjustedStart: string;
    adjustedEnd: string;
    start: string;
    end: string;
}

export type Keyword = {
    id: number;
    text: string;
    confidence: number;
    language: string;
    instances: KeywordInstance[];
}

type NamedLocationInstance = {
    instanceSource: string;
    adjustedStart: string;
    adjustedEnd: string;
    start: string;
    end: string;
}

export type NamedLocation = {
    id: number;
    name: string;
    referenceId: string | null;
    referenceUrl: string | null;
    description: string | null;
    tags: string[];
    confidence: number;
    isCustom: boolean;
    instances: NamedLocationInstance[];
}

type NamedPeopleInstance = {
    instanceSource: string;
    adjustedStart: string;
    adjustedEnd: string;
    start: string;
    end: string;
}

export type NamedPeople = {
    id: number;
    name: string;
    referenceId: string | null;
    referenceUrl: string | null;
    description: string | null;
    tags: string[];
    confidence: number;
    isCustom: boolean;
    instances: NamedPeopleInstance[];
}

type EmotionInstance = {
    confidence: number;
    adjustedStart: string;
    adjustedEnd: string;
    start: string;
    end: string;
}

export type Emotion = {
    id: number;
    type: string;
    instances: EmotionInstance[];
}

type TranscriptInstance = {
    adjustedStart: string;
    adjustedEnd: string;
    start: string;
    end: string;
}

export type Transcript = {
    id: number;
    text: string;
    confidence: number;
    speakerId: number;
    language: string;
    instances: TranscriptInstance[];
}

type ThumbnailInstance = {
    adjustedStart: string;
    adjustedEnd: string;
    start: string;
    end: string;
}

type Thumbnail = {
    id: string;
    fileName: string;
    instances: ThumbnailInstance[];
}

type FaceInstance = {
    thumbnailsIds: string[];
    adjustedStart: string;
    adjustedEnd: string;
    start: string;
    end: string;
}

export type Face = {
    id: number;
    name: string;
    confidence: number;
    description: string | null;
    thumbnailId: string;
    title: string | null;
    imageUrl: string | null;
    highQuality: boolean;
    thumbnails: Thumbnail[];
    instances: FaceInstance[];
}

export class VideoMetadateClass {
    topics: Topic[];
    labels: Label[];
    keywords: Keyword[];
    namedLocations: NamedLocation[];
    namedPeople: NamedPeople[];
    emotions: Emotion[];
    transcript: Transcript[];
    faces: Face[];

    constructor(data: any) {
        this.topics = data['videos'][0]['insights']['topics'];
        this.labels = data['videos'][0]['insights']['labels'];
        this.keywords = data['videos'][0]['insights']['keywords'];
        this.namedLocations = data['videos'][0]['insights']['namedLocations'];
        this.namedPeople = data['videos'][0]['insights']['namedPeople'];
        this.emotions = data['videos'][0]['insights']['emotions'];
        this.transcript = data['videos'][0]['insights']['transcript'];
        this.faces = data['videos'][0]['insights']['faces'];
    }
}