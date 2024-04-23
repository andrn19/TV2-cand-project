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

export type Metadata = {
    topics: Topic[] | undefined;
    labels: Label[] | undefined;
    keywords: Keyword[] | undefined;
    namedLocations: NamedLocation[] | undefined;
    namedPeople: NamedPeople[] |undefined;
    emotions: Emotion[] | undefined;
    transcript: Transcript[] | undefined;
    faces: Face[] |undefined;
}

export class VideoMetadateClass {
    videoId: string;
    videoName: string
    publishedUrl: string;
    metadata: Metadata;

    constructor(data: any) {
        this.videoId = data['id']
        this.videoName = data['name']
        this.publishedUrl = data['publishedUrl']
        this.metadata = data['insights']
    }
}